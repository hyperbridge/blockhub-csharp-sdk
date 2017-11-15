using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIInputControl : MonoBehaviour {
    public InputField InputSymbol;
    public FinanceCharter Charter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ButtonShowSymbolOneDayClick()
    {
       
        string symbol = InputSymbol.text;
        Charter.Period = FinanceCharter.FinancePeriod.Last1Days;
        Charter.Symbol = symbol;
      Charter.RefreshChartStart();
    }
    public void ButtonShowSymbolFiveDayClick()
    {
        
        string symbol = InputSymbol.text;
        Charter.Period = FinanceCharter.FinancePeriod.Last5Days;
        Charter.Symbol = symbol;
        Charter.RefreshChartStart();
    }
}
