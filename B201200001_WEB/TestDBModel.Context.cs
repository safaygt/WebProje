﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace B201200001_WEB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class web_programlamaEntities : DbContext
    {
        public web_programlamaEntities()
            : base("name=web_programlamaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Adres> Adres { get; set; }
        public virtual DbSet<Fatura> Fatura { get; set; }
        public virtual DbSet<ilce> ilce { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Magaza> Magaza { get; set; }
        public virtual DbSet<Mahalle> Mahalle { get; set; }
        public virtual DbSet<Oyuncak> Oyuncak { get; set; }
        public virtual DbSet<OyuncakRenk> OyuncakRenk { get; set; }
        public virtual DbSet<OyuncakTur> OyuncakTur { get; set; }
        public virtual DbSet<Sehir> Sehir { get; set; }
        public virtual DbSet<Siparis> Siparis { get; set; }
        public virtual DbSet<Sokak> Sokak { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}