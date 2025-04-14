namespace RegistrationPortal.Data.Models;
public class EmailTemplate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Subject { get; set; }

    public string Division { get; set; } // Can be "All" or specific division

    public string PlainTextTemplate { get; set; }

    public string HtmlTemplate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    // List of conditional content blocks that can be used in the template
    public List<ConditionalBlock> ConditionalBlocks { get; set; } = new List<ConditionalBlock>();
}

// Represents a conditional block of content in an email template
public class ConditionalBlock
{
    public string Name { get; set; }

    public string Description { get; set; }

    // The condition to evaluate
    public string ConditionType { get; set; } // "StateIs", "StateContains", "StateNotIn", etc.

    // States that trigger this condition (comma-separated list for multiple states)
    public string States { get; set; }

    // Content to display when condition is true
    public string TrueContent { get; set; }

    // Content to display when condition is false
    public string FalseContent { get; set; }
}