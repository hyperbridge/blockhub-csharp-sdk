using UnityEngine;
using System.Collections;

public class FinanceQuoter : MonoBehaviour
{
    public string Symbol;
    Quote retQuote;
    public UnityEngine.UI.Text TextSymbol;
    public UnityEngine.UI.Text TextAskRate;
    public bool AutoUpdate;
    const int FRAME_UPDATE_RATE = 500;
    int framesUntilNextUpdate = 0;
    void Start()
    {
        if (retQuote == null)
        {
            retQuote = new Quote();
        }
        StartCoroutine(RefershQuotes());
    }
    public void RefreshQuotesStart()
    {
        StartCoroutine(RefershQuotes());

    }
    private IEnumerator RefershQuotes()
    {
        string yahooURL = @"http://download.finance.yahoo.com/d/quotes.csv?s=" + Symbol + "&f=sl1d1t1c1hgvbap2";
        WWW www = new WWW(yahooURL);
        yield return www;
        print(www.text);
        string[] contents = www.text.ToString().Split(',');
        retQuote.Symbol = contents[0].Replace("\"", "");
        if (contents.Length > 1)
        {
            retQuote.Last = contents[1].Replace("\"", "");
            retQuote.Date = contents[2].Replace("\"", "");
            retQuote.Time = contents[3].Replace("\"", "");
            retQuote.Change = contents[4].Replace("\"", "");
            retQuote.High = contents[5].Replace("\"", "");
            retQuote.Low = contents[6].Replace("\"", "");
            retQuote.Volume = contents[7].Replace("\"", "");
            retQuote.Bid = contents[8].Replace("\"", "");
            retQuote.Ask = contents[9].Replace("\"", "");

        }
        TextSymbol.text = retQuote.Symbol;
        TextAskRate.text = retQuote.Ask;
        //print("retQuote.Symbol" + retQuote.Symbol);
        //print("retQuote.Last" + retQuote.Last);
        //print("retQuote.Date" + retQuote.Date);
        //print("retQuote.Time" + retQuote.Time);
        //print("retQuote.Change" + retQuote.Change);
        //print("retQuote.High" + retQuote.High);
        //print("retQuote.Low" + retQuote.Low);
        //print("retQuote.Volume" + retQuote.Volume);
        //print("retQuote.Bid" + retQuote.Bid);
        //print("retQuote.Ask" + retQuote.Ask);

    }
    void Update()
    {
        if (AutoUpdate)
        {
            if (framesUntilNextUpdate <= 0)
            {
                framesUntilNextUpdate = FRAME_UPDATE_RATE;
                StartCoroutine(RefershQuotes());
            }
            framesUntilNextUpdate--;
        }
    }
    public class Quote
    {
        public string Symbol;
        public string Last;
        public string Date;
        public string Time;
        public string Change;
        public string High;
        public string Low;
        public string Volume;
        public string Bid;
        public string Ask;


    }
}
