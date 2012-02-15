using System;

namespace Sweater_Vest_Mercs
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SVMGame game = new SVMGame())
            {
                game.Run();
            }
        }
    }
#endif
}

