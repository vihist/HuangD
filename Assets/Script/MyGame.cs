using System;
using System.Text;

public class MyGame
{
    public static MyGame Inst = new MyGame();

	public void Initialize(string strEmpName, string strYearName, string strPeriodName)
    {
        empName = strEmpName;
		m_strYearName = strYearName;
		m_strPeriodName = strPeriodName;

		empAge = Tools.Probability.GetRandomNum (16, 40);
		empHeath = Tools.Probability.GetRandomNum (50, 90);
    }

    private MyGame()
    {
    }

    private string GetText(string str)
    {

#if UNITY_EDITOR_OSX
		if(Tools.StringT.isChinese(str))
		{
			byte[] bytes = Encoding.Unicode.GetBytes(str);
			return BitConverter.ToString (bytes, 0).Replace ("-", string.Empty);
		}
#endif
		return str;
    }

	public string empName;
	public int empAge;
	public int empHeath;

	private string m_strYearName;
	private string m_strPeriodName;
	private int    m_iWending;
	private int    m_iFuku;
	private int    m_iWubei;
}
