﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PartyProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TeamProjectEntities : DbContext
    {
        public TeamProjectEntities()
            : base("name=TeamProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }
        public virtual DbSet<EntertainerDetail> EntertainerDetails { get; set; }
        public virtual DbSet<RelatedSkill> RelatedSkills { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tblLocation> tblLocations { get; set; }
        public virtual DbSet<tblSkill> tblSkills { get; set; }
        public virtual DbSet<CustView> CustViews { get; set; }
        public virtual DbSet<EntView> EntViews { get; set; }
    }
}
