using SunamoCl;

namespace RunnerSelenium;

partial class Program
{
    private static Dictionary<string, Func<Task<Dictionary<string, object>>>> AddGroupOfActions()
    {
        Dictionary<string, Func<Task<Dictionary<string, object>>>> groupsOfActions = new()
        {
            { "Other", Other },
        };

        return groupsOfActions;
    }

    static async Task<Dictionary<string, object>> Other()
    {
        var actions = OtherActions();

        if (CL.Perform)
        {
            await CLActions.PerformActionAsync(actions);
        }

        return actions;
    }

    private static Dictionary<string, object> OtherActions()
    {
        Dictionary<string, Action> actions = new();
        Dictionary<string, Func<Task>> actionsAsync = new();

        return CLActions.MergeActions(actions, actionsAsync);
    }
}
