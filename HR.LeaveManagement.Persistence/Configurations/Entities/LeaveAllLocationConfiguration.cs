using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations.Entities
{
    public class LeaveAllLocationConfiguration : IEntityTypeConfiguration<LeaveAllLocation>
    {
        public void Configure(EntityTypeBuilder<LeaveAllLocation> builder)
        {

        }
    }
}