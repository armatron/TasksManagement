using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasksManagement.Domain.Enums;

namespace TasksManagement.Domain.Entities
{
    public class TasksEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(200)")] //, StringLength(200)
        public string? Title { get; set; }
        [Column(TypeName = "VARCHAR(500)")] //, StringLength(500)
        public string? Description { get; set; }
        [DefaultValue("false")]
        public bool isCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        [EnumDataType(typeof(TasksPriority))]
        public TasksPriority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
