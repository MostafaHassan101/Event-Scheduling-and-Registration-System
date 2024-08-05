namespace EventSystem.Application.Common.Models;

public class APIResponse
{
    public Dictionary<string, dynamic> responsebody = new Dictionary<string, dynamic>();

    public int StatusCode { get; set; }
    public dynamic Model { get; set; }
    public Dictionary<string, dynamic> Metas { get; set; }
    public Dictionary<string, dynamic> Errors { get; set; }

    public void AddModel(string key, dynamic value)
    {
        try
        {
            responsebody.Remove(AppConstants.Model);
            responsebody.Add(AppConstants.Model, value);
        }
        catch
        {

        }
    }

    public void AddError(string key, dynamic value)
    {
        Dictionary<string, dynamic> ErrorParent = new Dictionary<string, dynamic>();
        if (!responsebody.ContainsKey(AppConstants.Error))
        {
            responsebody.Add(AppConstants.Error, ErrorParent);
        }

        foreach (KeyValuePair<string, dynamic> Error in responsebody)
        {
            if (Error.Key.ToString().Equals(AppConstants.Error))
            {
                ErrorParent = Error.Value;

            }
        }
        try
        {
            ErrorParent.Add(key, value);
        }
        catch { }
        responsebody.Remove(AppConstants.Error);
        responsebody.Add(AppConstants.Error, ErrorParent);
    }


    public void AddMeta(string key, dynamic value)
    {
        Dictionary<string, dynamic> MetasParent = new Dictionary<string, dynamic>();
        if (!responsebody.ContainsKey(AppConstants.Metas))
        {
            responsebody.Add(AppConstants.Metas, MetasParent);
        }

        foreach (KeyValuePair<string, dynamic> Metas in responsebody)
        {
            if (Metas.Key.ToString().Equals(AppConstants.Metas))
            {
                MetasParent = Metas.Value;

            }
        }
        try
        {
            MetasParent.Add(key, value);
        }
        catch { }
        responsebody.Remove(AppConstants.Metas);
        responsebody.Add(AppConstants.Metas, MetasParent);
    }

    public void clearBody()
    {
        responsebody.Clear();
        responsebody.Add(AppConstants.Model, new List<string>());
        responsebody.Add(AppConstants.Metas, new Dictionary<string, dynamic>());
        responsebody.Add(AppConstants.Error, new Dictionary<string, dynamic>());
    }

}
