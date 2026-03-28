using SunamoCl;

namespace RunnerSelenium;

partial class Program
{
    /// <summary>
    /// Creates and returns groups of available actions for the command-line interface.
    /// </summary>
    /// <returns>A dictionary mapping group names to their action providers.</returns>
    private static Dictionary<string, Func<Task<Dictionary<string, object>>>> AddGroupOfActions()
    {
        Dictionary<string, Func<Task<Dictionary<string, object>>>> groupsOfActions = new()
        {
            { "Other", Other },
        };

        return groupsOfActions;
    }

    /// <summary>
    /// Executes the "Other" action group.
    /// </summary>
    /// <returns>A dictionary of available actions in this group.</returns>
    static async Task<Dictionary<string, object>> Other()
    {
        var actions = OtherActions();

        if (CL.Perform)
        {
            await CLActions.PerformActionAsync(actions);
        }

        return actions;
    }

    /// <summary>
    /// Builds the dictionary of actions available in the "Other" group.
    /// </summary>
    /// <returns>A merged dictionary of synchronous and asynchronous actions.</returns>
    private static Dictionary<string, object> OtherActions()
    {
        Dictionary<string, Action> actions = new Dictionary<string, Action>();
        Dictionary<string, Func<Task>> actionsAsync = new Dictionary<string, Func<Task>>();

        return CLActions.MergeActions(actions, actionsAsync);
    }
}
