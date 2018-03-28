using System;
using Xamarin.Forms;
using System.Collections;

namespace Memoria_lab_4
{
    public partial class MainPage : ContentPage
    {
        const int FILAS = 6;
        const int COLS = 6;
        char[,] modelo = new char[FILAS, COLS];
        int modo = 0;
        Button primero;
        Button segundo;
        String primeroChr;
        String segundoChr;
        int parejasEncontradas = 0;
        public MainPage()
        {
            InitializeComponent();
            llenar();

        }


        public void onReset(Object sender, EventArgs e) { 
            parejasEncontradas = 0;
            modo = 0;
            llenar();
            etiqueta.Text = "JUGAR MEMORIA";
            limpiar();
        }

        void limpiar() {
            for (int i = 1; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    String name = "B" + i.ToString() + j.ToString();
                    Button b2 = this.FindByName<Button>(name);
                    this.ocultarCasilla(b2);
                }
            }

        }


        public void onSolution(Object sender, EventArgs e)
        {
            for (int i = 1; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    String name = "B" + i.ToString() + j.ToString();
                    Button b2 = this.FindByName<Button>(name);
                    this.volteaCasilla(b2);

                }

            }
            this.final();
        }

        public void onClick(Object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if(b.Text != " ")
            {
                return;
            }
            switch (modo)
            {
                case 0: modo1(b); break;
                case 1: modo2(b); break;
                case 2: modo3(b); break;
            }
           
        }
        void modo1(Button b)
        {
            primero = b;
            volteaCasilla(b);
            primeroChr = b.Text;
            ++modo;
        }
        void modo2(Button b)
        {
            segundo = b;
            volteaCasilla(b);
            segundoChr = b.Text;
            ++modo;
            if(parejasEncontradas == 17){
                this.final();
            }
        }
        void modo3(Button b)
        {
            if (primeroChr == segundoChr) {
                parejasEncontradas++;
            }
            else
            {
                ocultarCasilla(primero);
                ocultarCasilla(segundo);
            }
            modo=0;
            modo1(b);
        }

        String find(Button b){
            for (int i = 1; i < 7;i++){
                for (int j = 0; j < 6; j++){
                    String name = "B" + i.ToString() + j.ToString();
                    Button b2 = this.FindByName<Button>(name);
                    if(b2 == b){
                        return name;
                    }
                }
                    
            }
            return "B00";
        }    

        void volteaCasilla(Button b) {
            b.BackgroundColor = Color.Yellow;
            String name = find(b);
        
            String Fila = name.Substring(1, 1);//B[1]0
            String Colm = name.Substring(2);  //B0[1] 
            int filaInt, colInt;
            Int32.TryParse(Fila, out filaInt);
            Int32.TryParse(Colm, out colInt);
            var letra = modelo[filaInt -1, colInt];
            b.Text = letra.ToString();
        }
        void ocultarCasilla(Button b)
        {
            b.BackgroundColor = Color.White;
            b.Text = " ";
        }

        void final(){
            etiqueta.Text = "Final del juego.";
        }

        void llenar()
        {
            Random r = new Random();
            ArrayList letras = new ArrayList();
            for (char i = 'A'; i <= 'Z'; i++) {
                letras.Add(i);
            }
            ArrayList posiciones = new ArrayList();
            for (int i = 0; i < FILAS; i++) {
                for (int j = 0; j < COLS; j++) {
                    int[] pos = new int[2];
                    pos[0] = i;
                    pos[1] = j;
                    posiciones.Add(pos);
                } 
            }
            while(posiciones.Count > 0)
            {
                int index = r.Next(0, letras.Count);
                var letra = letras[index];
                letras.RemoveAt(index);//para evitar más de una repetición
                index = r.Next(0,posiciones.Count);
                int [] pos = (int[])posiciones[index];
                posiciones.RemoveAt(index);
                index = r.Next(0, posiciones.Count);
                modelo[pos[0],pos[1]] = (char)letra;//asgino
                pos = (int[])posiciones[index];
                posiciones.RemoveAt(index);
                modelo[pos[0], pos[1]] = (char)letra; //asigno
            }

        }



    }
}