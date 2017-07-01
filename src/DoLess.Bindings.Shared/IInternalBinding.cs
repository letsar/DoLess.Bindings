using System;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding between a source and a target.
    /// </summary>
    internal interface IInternalBinding : 
        IBinding,
        IDisposable
    {
        long Id { get; }

        BindingGroup BindingGroup { get; }

        bool CanBePurged();

        void DeleteFromGroup();
    }
}
