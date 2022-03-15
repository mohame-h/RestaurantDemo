namespace Domain.Entities
{
    public class Feature : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultAssociatedRole { get; set; } //UserRoleTypes enum

    }
}
