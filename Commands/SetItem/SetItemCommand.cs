namespace OBRemote.Commands.SetItem;

public abstract class SetItemCommand : RPCCommand {
    /// Emit a set-item RPC
    public void Set(string item, string key, string value) {
        Send(new MagicCircle.SetItemRPC{
            cmd = "set-item",
            item = item,
            key = key,
            value = value
        });
    }
    
    /**
     * Emit a set-item RPC, with all the properties inferred from program arguments
     *
     * <param name="args"> The argument list to the program </param>
     * <param name="oneShot"> if true, open and close the RPC channel </param>
     * <param name="options"> </param>
     **/
    public void Set(List<string> args, bool oneShot = true, Dictionary<string, string>? options = null) {
        if(oneShot) base.Execute(args, options);
        Set(args[1], args[0], String.Join(' ', args.Skip(2)));
        if(oneShot) Close();
    }
    
    /**
     * Emit a set-item RPC, with most of the properties inferred from program arguments
     *
     * <param name="key"> The item key that will be set </param>
     * <param name="args"> The argument list to the program </param>
     * <param name="oneShot"> if true, open and close the RPC channel </param>
     * <param name="options"> </param>
     **/
    public void Set(string key, List<string> args, bool oneShot = true, Dictionary<string, string>? options = null) {
        if(oneShot) base.Execute(args, options);
        Set(args[1], key, String.Join(' ', args.Skip(1)));
        if(oneShot) Close();
    }
}