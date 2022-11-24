using System;
using System.Collections.Generic;
using System.Text;
using Notes.Models;
using SQLite;
using System.Threading.Tasks;
using SQLiteNetExtensions;
using SQLiteNetExtensionsAsync;
using SQLitePCL;
using System.Linq;
using SQLiteNetExtensionsAsync.Extensions;

namespace Notes.Data
{
    public class DateBase
    {
        readonly SQLiteAsyncConnection db;

        public DateBase(string connectinString)
        {
            db = new SQLiteAsyncConnection(connectinString);
            db.CreateTableAsync<User>().Wait();
            db.CreateTableAsync<NoteNews>().Wait();
            db.CreateTableAsync<ServiceCategory>().Wait();
            db.CreateTableAsync<Service>().Wait();
            db.CreateTableAsync<GoodsCategory>().Wait();
            db.CreateTableAsync<Goods>().Wait();

            SaveUserAsync(new User() { Email = "Admin", Password = "123456" });
            SaveUserAsync(new User() { Email = "User", Password = "123456" });
        }
        #region Service
        public Task<List<ServiceCategory>> GetServiceCategoriesAsync()
        {
            return db.GetAllWithChildrenAsync<ServiceCategory>();
        }

        public Task<ServiceCategory> GetServiceCategoryAsync(int id)
        {
            return db.Table<ServiceCategory>()
                .Where(i => id == i.ServiceCategoryId)
                .FirstOrDefaultAsync();
        }

        public Task SaveServiceCategoryAsync(ServiceCategory sc)
        {
            return db.InsertWithChildrenAsync(sc);
        }

        public Task<int> DeleteServiceCategoryAsync(ServiceCategory sc)
        {
            return db.DeleteAsync(sc);
        }

        public Task<List<Service>> GetServicesAsync()
        {
            return db.Table<Service>().ToListAsync();
        }

        public Task<Service> GetServiceAsync(int id)
        {
            return db.Table<Service>()
                .Where(i => id == i.ServiceId)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveServiceAsync(Service sc)
        {
            return db.InsertAsync(sc);
        }

        public Task<int> DeleteServiceAsync(Service sc)
        {
            return db.DeleteAsync(sc);
        }

        public Task<int> DeleteAllCategoryServices()
        {
            return db.DeleteAllAsync<ServiceCategory>();
        }
        public Task<int> DeleteAllServices()
        {
            return db.DeleteAllAsync<Service>();
        }

        #endregion
        #region User
        public Task<List<User>> GetUsersAsync()
        {
            return db.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(string login, string password)
        {
            return db.Table<User>()
                .Where(user => login == user.Email && user.Password == password)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.ID != 0)
                return db.UpdateAsync(user);
            else
                return db.InsertAsync(user);
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return db.DeleteAsync(user);
        }
        #endregion
        #region NoteNews
        public Task<List<NoteNews>> GetNotesNewsesAsync()
        {
            return db.Table<NoteNews>().ToListAsync();
        }

        public Task<NoteNews> GetNoteNewsAsync(int id)
        {
            return db.Table<NoteNews>()
                .Where(i => id == i.NoteNewsId)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteNewsAsync(NoteNews noteNews)
        {
            if (noteNews.NoteNewsId != 0)
                return db.UpdateAsync(noteNews);
            else
                return db.InsertAsync(noteNews);
        }

        public Task<int> DeleteNoteNewsAsync(NoteNews noteNews)
        {
            return db.DeleteAsync(noteNews);
        }

        public Task<int> DeleteAllNoteNewsAsync()
        {
            return db.DeleteAllAsync<NoteNews>();
        }
        #endregion
        #region GoodsProudct
        public Task<List<GoodsCategory>> GetGoodsCategoriesAsync()
        {
            return db.GetAllWithChildrenAsync<GoodsCategory>();
        }

        public Task<GoodsCategory> GetGoodsCategoryAsync(int id)
        {
            return db.Table<GoodsCategory>()
                .Where(i => id == i.GoodsCategoryId)
                .FirstOrDefaultAsync();
        }

        public Task SaveGoodsCategoryWithChildrenAsync(GoodsCategory gc)
        {
            return db.InsertWithChildrenAsync(gc);
        }

        public Task<int> DeleteGoodsCategoryAsync(GoodsCategory gc)
        {
            return db.DeleteAsync(gc);
        }

        public Task<List<Goods>> GetGoodsAsync()
        {
            return db.Table<Goods>().ToListAsync();
        }

        public Task<Goods> GetGoodsAsync(int id)
        {
            return db.Table<Goods>()
                .Where(i => id == i.GoodsId)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveGoodsAsync(Goods g)
        {
            if (g.GoodsId != 0)
                return db.UpdateAsync(g);
            else
                return db.InsertAsync(g);
        }

        public Task<int> DeleteGoodsAsync(Goods g)
        {
            return db.DeleteAsync(g);
        }

        public Task<int> DeleteAllGoodsCategories()
        {
            return db.DeleteAllAsync<GoodsCategory>();
        }
        public Task<int> DeleteAllGoods()
        {
            return db.DeleteAllAsync<Goods>();
        }
        #endregion
    }
}
