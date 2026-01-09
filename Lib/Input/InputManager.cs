using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird.Lib.Input;

public class InputManager
{
    public KeyboardInfo KeyboardInfo { get; set; }

    public InputManager()
    {
        KeyboardInfo = new KeyboardInfo();
    }

    public void Update()
    {
        KeyboardInfo.Update();
    }
}
