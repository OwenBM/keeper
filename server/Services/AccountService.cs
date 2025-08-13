namespace keeper.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  private Account GetAccount(string accountId)
  {
    Account account = _repo.GetById(accountId);
    if (account == null)
    {
      throw new Exception("Invalid Account Id");
    }
    return account;
  }

  internal Account GetOrCreateAccount(Account userInfo)
  {
    Account account = _repo.GetById(userInfo.Id);
    if (account == null)
    {
      return _repo.Create(userInfo);
    }
    return account;
  }

  internal Account Edit(Account editData, Account userInfo)
  {
    Account original = GetAccount(userInfo.Id);
    if (original.Id != userInfo.Id) throw new Exception("You cannot edit another user's account!");
    original.Name = editData.Name ?? original.Name;
    original.Picture = editData.Picture ?? original.Picture;
    original.CoverImg = editData.CoverImg ?? original.CoverImg;
    _repo.Edit(original);
    return original;
  }
}
