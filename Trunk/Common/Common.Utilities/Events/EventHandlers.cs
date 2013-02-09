
namespace SportsWebPt.Common.Utilities
{

    /// <summary>
    /// A generic delegate that can be used to represent any event handler or method signature 
    /// with a matching number of parameters and no return type.
    /// </summary>
    /// <typeparam name="TArg">Type of first parameter.</typeparam>
    /// <param name="arg">First parameter.</param>
    public delegate void GenericEventHandler<TArg>(TArg arg);

    /// <summary>
    /// A generic delegate that can be used to represent any event handler or method signature 
    /// with a matching number of parameters and no return type.
    /// </summary>
    /// <typeparam name="TArg">Type of first parameter.</typeparam>
    /// <typeparam name="TReturn">Type of return parameter.</typeparam>
    /// <param name="arg">First parameter.</param>
    /// <returns></returns>
    public delegate TReturn GenericEventHandler<TArg, TReturn>(TArg arg);

}
