using System.Collections.Generic;
using System.Linq;

namespace Dialogue
{
    public class DialogueParser: Singleton<DialogueParser>
    {
    
        public void ParseDialogue(string dialog)
        {
            List<string> split = new List<string>(dialog.Split(' '));
            var words = split.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            
        }
    
    }
}
