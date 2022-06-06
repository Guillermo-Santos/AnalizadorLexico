using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace AnalizadorLexico
{
    internal class Lexico
    {
        const int TOKREC = 5;
        const int MAXTOKENS = 500;
        string[] _lexemas;
        string[] _tokens;
        string _lexema;
        int _noTokens;
        int _i;
        int _iniToken;

        Automata oAFD;
        public Lexico()
        {
            _lexemas = new string[MAXTOKENS];
            _tokens = new string[MAXTOKENS];
            oAFD = new Automata();
            _i = 0;
            _iniToken = 0;
            _noTokens = 0;
        }
        public int NoTokens
        {
            get { return _noTokens; }
        }
        public string[] Lexemas
        {
            get { return _lexemas; }
        }
        public string[] Tokens
        {
            get { return _tokens; }
        }
        public void Inicia()
        {
            _i = 0;
            _iniToken = 0;
            _noTokens = 0;
        }
        public void Analyze(string text)
        {
            bool recAuto;
            int noAuto;

            while (_i < text.Length)
            {
                recAuto = false; 
                noAuto = 0;

                for(; noAuto < TOKREC && !recAuto;)
                {
                    if (oAFD.Reconoce(text, _iniToken, ref _i, noAuto))
                        recAuto = true;
                    else
                        noAuto++;
                }
                if (recAuto)
                {
                    _lexema = text.Substring(_iniToken, _i - _iniToken);
                    switch (noAuto)
                    {
                        //--- Automata delim---
                        case 0:
                            break;
                        //--- Automata id---
                        case 1:
                            if (IsId(_lexema))
                                _tokens[_noTokens] = "id";
                            else
                                _tokens[_noTokens] = _lexema;
                            break;
                        //--- Automata num---

                        case 2:
                            _tokens[_noTokens] = "num";
                            break;

                        //--- Automata otros---

                        case 3:
                            _tokens[_noTokens] = _lexema;
                            break;

                        //--- Automata cad---

                        case 4:
                            _tokens[_noTokens] = "cad";
                            break;
                    }
                    if (noAuto != 0)
                        _lexemas[_noTokens++] = _lexema;
                }
                else
                {
                    _i++;
                }
                _iniToken = _i;

            }
        }

        public bool IsId(string Lexeme)
        {
            string[] reswords = {
                "inicio",
                "fin",
                "const",
                "var",
                "entero",
                "real",
                "cadena",
                "leer",
                "visua"
            };
            for (int i = 0; i < reswords.Length; i++)
                if (Lexeme == reswords[i])
                    return false;
            return true;
        }
    }
}
