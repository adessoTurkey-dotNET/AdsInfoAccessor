namespace AdsInfoAccessor
{
    /// <summary>
    /// Use to obtain info object.
    /// <see cref="IAdsInfo"/>
    /// </summary>
    public interface IAdsInfoAccessor
    {
        /// <summary>
        /// Get info object instance of <see cref="IAdsInfo"/>
        /// </summary>
        /// <typeparam name="TInfo"></typeparam>
        /// <returns>Instance of requested info object.</returns>
        TInfo GetInfo<TInfo>() where TInfo : IAdsInfo, new();
    }
}
