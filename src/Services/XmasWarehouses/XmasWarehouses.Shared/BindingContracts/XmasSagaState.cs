namespace XmasWarehouses.Shared.BindingContracts;

public record XmasSagaState(string Body, bool XmasLetterReceived, bool XmasLetterApproved, bool XmasLetterProcessed, bool XmasSagaClosed);