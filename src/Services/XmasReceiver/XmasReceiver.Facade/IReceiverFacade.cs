﻿using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.Shared.BindingContracts;

namespace XmasReceiver.Facade;

public interface IReceiverFacade
{
    Task<PagedResult<XmasLetterContract>> GetXmasLetterAsync(CancellationToken cancellationToken);
}