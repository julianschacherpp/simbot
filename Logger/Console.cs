using System;
using System.Text;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;

namespace simbot.Log
{
    public static class Console
    {
        public static void Log(Category category, string message, bool important = false)
        {
            var logMessage = new StringBuilder();

            if (important)
                logMessage.Append("!");

            logMessage.Append(DateTime.Now.ToString("s") + " ");
            logMessage.Append("[" + category.ToString() + "] ");
            logMessage.Append(message);

            System.Console.WriteLine(logMessage.ToString());
        }

        public static void LogCommand(string command, CommandContext context)
        {
            var logMessage = new StringBuilder();

            logMessage.Append("Command: \"" + command + "\" ");
            logMessage.Append("triggered by \"" + context.Message.Author + "\" ");
            logMessage.Append("in channel: \"" + context.Channel + "\" ");
            logMessage.Append("in guild: \"" + context.Guild + "\" ");
            logMessage.Append("(message contents: \"" + context.Message.Content + "\")");

            Log(Category.Discord, logMessage.ToString());
        }

        public static void LogDynamicCommand(string command, MessageCreateEventArgs args)
        {
            var logMessage = new StringBuilder();

            logMessage.Append("DynamicCommand: \"" + command + "\" ");
            logMessage.Append("triggered by \"" + args.Message.Author + "\" ");
            logMessage.Append("in channel: \"" + args.Channel + "\" ");
            logMessage.Append("in guild: \"" + args.Guild + "\" ");
            logMessage.Append("(message contents: \"" + args.Message.Content + "\")");

            Log(Category.Discord, logMessage.ToString());
        }
    }
}
