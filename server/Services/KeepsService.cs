



using Microsoft.AspNetCore.Http.HttpResults;

namespace keeper.Services;

public class KeepsService
{
    private readonly KeepsRepository _keepRepo;

    public KeepsService(KeepsRepository keepRepo)
    {
        _keepRepo = keepRepo;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep newKeep = _keepRepo.CreateKeep(keepData);
        return newKeep;
    }

    internal string DeleteKeep(int keepId, Account userInfo)
    {
        Keep keep = GetKeepById(keepId);
        if (keep.CreatorId != userInfo.Id) throw new Exception("You cannot delete another user's keep");
        _keepRepo.DeleteKeep(keepId);
        return $"The keep with an id of '{keepId}' has been deleted!";
    }

    internal Keep EditKeep(Keep keepData, int keepId)
    {
        Keep originalKeep = GetKeepById(keepId);
        if (originalKeep.CreatorId != keepData.CreatorId) throw new Exception("You can not edit another user's keep");
        originalKeep.Name = keepData.Name ?? originalKeep.Name;
        originalKeep.Description = keepData.Description ?? originalKeep.Description;
        originalKeep.Img = keepData.Img ?? originalKeep.Img;

        _keepRepo.EditKeep(originalKeep);
        return originalKeep;
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _keepRepo.GetAllKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _keepRepo.GetKeepById(keepId);
        if (keep == null) throw new Exception($"Invalid id: {keepId}");
        return keep;
    }
}