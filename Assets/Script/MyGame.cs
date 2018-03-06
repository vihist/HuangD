using System;
using System.Text;

public class MyGame
{
    public static MyGame Inst = new MyGame();

    public string strEmpName{
        get
        {
#if UNITY_EDITOR_OSX
			return GetText(m_strEmpName);
#else
            return m_strEmpName;
#endif
        }
    }

    public void Initialize(string strEmpName)
    {
        m_strEmpName = strEmpName;
    }

    private MyGame()
    {

    }

    private string GetText(string str)
    {
        byte[] bytes = Encoding.Default.GetBytes(str);
        return Encoding.Default.GetString(bytes);
    }

    private string m_strEmpName;
}
