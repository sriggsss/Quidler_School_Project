using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    public class Card
    {

        public string CardValue;
        public int CardPointValue;


        public Card(string cardLetter)
        {
            CardValue = cardLetter;

            //determining point value of the card here
            //NOTE: really ugly but should get the job done
            if (CardValue.Equals("a") || CardValue.Equals("e") || CardValue.Equals("i") || CardValue.Equals("o"))
                CardPointValue = 2;
            else if (CardValue.Equals("l") || CardValue.Equals("s") || CardValue.Equals("t"))
                CardPointValue = 3;
            else if (CardValue.Equals("u") || CardValue.Equals("y"))
                CardPointValue = 4;
            else if (CardValue.Equals("d") || CardValue.Equals("m") || CardValue.Equals("n") || CardValue.Equals("r"))
                CardPointValue = 5;
            else if (CardValue.Equals("f") || CardValue.Equals("g") || CardValue.Equals("p"))
                CardPointValue = 6;
            else if (CardValue.Equals("h") || CardValue.Equals("er") || CardValue.Equals("in"))
                CardPointValue = 7;
            else if (CardValue.Equals("b") || CardValue.Equals("c") || CardValue.Equals("k"))
                CardPointValue = 8;
            else if (CardValue.Equals("qu") || CardValue.Equals("th"))
                CardPointValue = 9;
            else if (CardValue.Equals("w") || CardValue.Equals("cl"))
                CardPointValue = 10;
            else if (CardValue.Equals("v"))
                CardPointValue = 11;
            else if (CardValue.Equals("x"))
                CardPointValue = 12;
            else if (CardValue.Equals("j"))
                CardPointValue = 13;
            else if (CardValue.Equals("z"))
                CardPointValue = 14;
            else if (CardValue.Equals("q"))
                CardPointValue = 15;
            else
                CardPointValue = 0;
            
        }
    }
}
