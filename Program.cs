using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Solução__HTTP_;

namespace Solução__HTTP_
{
    class Program
    {
        static void Main(string[] args)
        {
            //RequisaicaoLista();
            RequisicaoUnica();
        }
        static void RequisaicaoLista() 
        {
            //cadastrar requisição
            var requisicao = WebRequest.Create("https://jsonplaceholder.typicode.com/todos/");
            requisicao.Method = "GET";

            //executar requisição
            var resposta = requisicao.GetResponse();
            using (resposta) // using() abre e fecha a conexão automaticamente
            {
                var stream = resposta.GetResponseStream();//pegando a resposta do site -GetResponseStream- coloca na varialve stream
                StreamReader leitor = new StreamReader(stream); //pegando a resposta -stream- e coloquei dentro do leitor -StreamReader
                object dados = leitor.ReadToEnd();// lendo a resposta

                //Console.WriteLine(dados.ToString());//pego a resposta transformo em string e printo no console

                List<Tarefa> tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(dados.ToString()); //Convertendo o formato Jsaon para string

                foreach (Tarefa tarefa in tarefas)
                {
                    tarefa.Exibir();
                }

                Console.WriteLine(tarefas);

                stream.Close(); //fechando conexão
                resposta.Close(); //encerrando resposta
            }
        }
        static void RequisicaoUnica()
        {
            var requisicao = WebRequest.Create("https://jsonplaceholder.typicode.com/todos/40");
            requisicao.Method = "GET";

            var resposta = requisicao.GetResponse();
            using (resposta)
            {
                var stream = resposta.GetResponseStream();
                StreamReader leitor = new StreamReader(stream);
                object dados = leitor.ReadToEnd();

                Tarefa tarefa = JsonConvert.DeserializeObject<Tarefa>(dados.ToString());

                tarefa.Exibir();

                stream.Close();
                resposta.Close();
            }
        }
    }
}
