namespace RegistrationPortal.Services;

public interface IEmailTemplateProcessor
{
    string ProcessTemplate(string template, Dictionary<string, string> variables, List<ConditionalBlock> conditionalBlocks, List<Form> forms);
}