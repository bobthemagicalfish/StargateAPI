using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StargateAPI.Business.Data
{
    [Table("Logging")]
    public class Logging
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string TypeOfMessage { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        
           
    }
    public class LoggingConfiguration : IEntityTypeConfiguration<Logging>
    {
        public void Configure(EntityTypeBuilder<Logging> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
