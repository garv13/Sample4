using Sample4.Models;
using Sample4.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample4.Utilities
{
    public class DatabaseHelper
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseHelper()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Message).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Message)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }
        public Task<List<Message>> GetItemsAsync()
        {
            return Database.Table<Message>().ToListAsync();
        }

        public async Task SaveItemAsync(Message item)
        {
            if (await ItemExistsAsync(item.Id))
            {
                await Database.UpdateAsync(item);
            }
            else
            {
                await Database.InsertAsync(item);
            }
        }
        public async Task<bool> ItemExistsAsync(double id)
        {
            Message m = await Database.Table<Message>().Where(i => i.Id == id).FirstOrDefaultAsync();
            if (m != null)
                return true;
            else
                return false;
        }
        public Task<int> DeleteItemAsync(Message item)
        {
            return Database.DeleteAsync(item);
        }
        public async Task<ObservableCollection<MessageViewModel>> getItemView()
        {
            ObservableCollection<MessageViewModel> MessageViews = new ObservableCollection<MessageViewModel>();

            try
            {
                List<Message> Message = await GetItemsAsync();
                foreach (Message m in Message)
                {
                    MessageViewModel mv = new MessageViewModel(m);
                    MessageViews.Add(mv);
                }
            }
            catch (Exception ex)
            {

            }
            return MessageViews;
        }
    }
}
