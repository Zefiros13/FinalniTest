namespace FinalniTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMyModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizacionaJedinicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(nullable: false, maxLength: 50),
                        GodinaOsnivanja = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zaposlenis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImeIPrezime = c.String(nullable: false, maxLength: 70),
                        Rola = c.String(nullable: false, maxLength: 50),
                        GodinaRodjenja = c.Int(nullable: false),
                        GodinaZaposlenja = c.Int(nullable: false),
                        Plata = c.Decimal(nullable: false, precision: 18, scale: 2),
                        JedinicaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizacionaJedinicas", t => t.JedinicaId, cascadeDelete: true)
                .Index(t => t.JedinicaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zaposlenis", "JedinicaId", "dbo.OrganizacionaJedinicas");
            DropIndex("dbo.Zaposlenis", new[] { "JedinicaId" });
            DropTable("dbo.Zaposlenis");
            DropTable("dbo.OrganizacionaJedinicas");
        }
    }
}
