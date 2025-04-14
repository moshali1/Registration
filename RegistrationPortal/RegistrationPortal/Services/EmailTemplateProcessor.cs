using System.Text.RegularExpressions;

namespace RegistrationPortal.Services;

public class EmailTemplateProcessor : IEmailTemplateProcessor
{
    public string ProcessTemplate(string template, Dictionary<string, string> variables, List<ConditionalBlock> conditionalBlocks, List<Form> forms)
    {
        // Replace variables in the template
        foreach (var variable in variables)
        {
            template = template.Replace($"{{{{{variable.Key}}}}}", variable.Value);
        }

        // Process conditional blocks
        if (conditionalBlocks != null && conditionalBlocks.Any())
        {
            foreach (var block in conditionalBlocks)
            {
                // Extract states from the conditional block
                var statesList = block.States?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>();

                // Check if any form matches the condition
                bool conditionMet = false;

                if (block.ConditionType == "StateIs" || block.ConditionType == "StateContains")
                {
                    // Check if any form's state matches any of the specified states
                    conditionMet = forms.Any(f => statesList.Contains(f.AddressInfo.StateProvince));
                }
                else if (block.ConditionType == "StateNotIn")
                {
                    // Check if all forms' states are NOT in the specified list
                    conditionMet = !forms.Any(f => statesList.Contains(f.AddressInfo.StateProvince));
                }

                // Replace the conditional block in the template with the appropriate content
                var contentToUse = conditionMet ? block.TrueContent : block.FalseContent;

                // Create a pattern to match the conditional block
                template = template.Replace($"{{{{{block.Name}}}}}", contentToUse);
            }
        }

        // Handle newlines - only perform this if the template appears to be HTML content
        if (template.Contains("<") && template.Contains(">"))
        {
            // For HTML templates, replace literal \n with <br /> tags
            template = template.Replace("\\n", "<br />");
        }

        return template;
    }
}
