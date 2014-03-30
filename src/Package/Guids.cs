// Guids.cs
// MUST match guids.h
using System;

namespace ChrisTaylor.TfsQuickTasks
{
    static class GuidList
    {
        public const string guidTfsQuickTasksPkgString = "b22f8081-fc99-4ab8-928e-44fccc1be7b7";
        public const string guidTfsQuickTasksCmdSetString = "bcd6b37b-7f6e-45f3-93cd-d88ba776f11e";
        //public const string guidToolWindowPersistanceString = "9c62a194-f533-4a38-9509-69c127ade50a";
        public const string guidToolWindowTfsQuickTaskExplorerPersistenceString = "4B0E03AE-D87E-4E1E-8061-EB7A01B378B6";
        public static readonly Guid guidTfsQuickTasksCmdSet = new Guid(guidTfsQuickTasksCmdSetString);
    };
}