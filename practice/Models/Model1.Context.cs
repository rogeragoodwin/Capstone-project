﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace practice.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectExampleEntities : DbContext
    {
        public ProjectExampleEntities()
            : base("name=ProjectExampleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Sibling> Siblings { get; set; }
        public virtual DbSet<SiblingGroup> SiblingGroups { get; set; }
        public virtual DbSet<UnavailableDate> UnavailableDates { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
    }
}
