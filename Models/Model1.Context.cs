﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace 游戏发布站.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class gameEntities : DbContext
    {
        public gameEntities()
            : base("name=gameEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdType> AdType { get; set; }
        public virtual DbSet<GameList> GameList { get; set; }
        public virtual DbSet<Navigation> Navigation { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<AdList> AdList { get; set; }
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<PayLog> PayLog { get; set; }
        public virtual DbSet<ZZlog> ZZlog { get; set; }
        public virtual DbSet<Userlog> Userlog { get; set; }
    }
}
