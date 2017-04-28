using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byssz.Wechat.database
{
    public class BysSZContext : DbContext
    {
        public BysSZContext()
            : base("name=VoteFavoriteDBM")
        {
            Database.SetInitializer<BysSZContext>(new bysofcontextInitializer());
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 1200;
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DbSet<CandidateInfo> CandidateInfos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<VoteItem> VoteItem { get; set; }
        public DbSet<VoteRequest> VoteRequest { get; set; }

        public class bysofcontextInitializer : CreateDatabaseIfNotExists<BysSZContext>
        {
            /// <summary>
            /// 使用继承的基类的初始化方法
            /// </summary>
            /// <param name="context">当前数据库上下文</param>
            public override void InitializeDatabase(BysSZContext context)
            {
                base.InitializeDatabase(context);
            }
        }
    }
}
