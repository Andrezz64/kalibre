"use client"
import { useState } from "react";

export default function Relatorio() {
  
    const[dataInicial,setDataInicial] = useState("");
    const[dataFinal,setDataFinal] = useState("");
    const [data,setData] = useState({
        "despesaAnalitycs": {
			"quantidadeDespesa": "",
			"valorDespesa": ""
		},
		"receitaAnalitycs": {
			"quantidadeReceita": "",
			"valorReceita": ""
		}
    })

  const handleSubmit = async (e:any) => {
    e.preventDefault()
    const options = {
        method: 'POST',
        headers: {'Content-Type': 'application/json', 'User-Agent': 'insomnia/8.6.1'},
        body: `{"dataInicial":"${dataInicial}","dataFinal":"${dataFinal}"}`
      };
      
      fetch('http://172.16.32.16:5014/api/v1/Relatorio', options)
        .then(response => response.json())
        .then(response => setData(response.data))
        .catch(err => console.error(err));
  }
  
    return (
    <div className="p-10">
      <main>
        <h1 className="text-lg text-center mb-10">Gerar Relatório</h1>
        <form onSubmit={handleSubmit} className="mb-10 flex max-md:flex-col max-md:justify-center max-md:items-center">
          <div>
            <strong>Período: </strong>
            <input
              type="date"
              required
              onChange={(e)=>{setDataInicial(e.target.value)}}
              title="Dada inicial"
              className="pl-4 border-stone-300 border-2  rounded-lg bg-stone-100"
            />
          </div>
          <br />
          <div>
            <label>{" --> "}</label>
            <input
              type="date"
              onChange={(e)=>{setDataFinal(e.target.value)}}
              required
              title="Data final"
              className="pl-4 border-stone-300 border-2  rounded-lg bg-stone-100"
            />
          </div>
        <br  />
          <button className="bg-sky-500 rounded-md border-2 min-w-[8rem] ml-5 border-transparent text-black hover:bg-transparent hover:border-sky-500 hover:text-sky-500 duration-300 ">Gerar</button>
        </form>
       <strong>Relatório de despesas</strong>
       <h1>Dispesas geradas no periodo</h1> {data.despesaAnalitycs.quantidadeDespesa}
        <h1>Valor em dispesas no periodo</h1>R${data.despesaAnalitycs.valorDespesa}
        <h1>------------------</h1>
        <strong>Relatório de receitas</strong>
        <h1>Receitas geradas no periodo</h1> {data.receitaAnalitycs.quantidadeReceita}
        <h1>Valor em receitas no periodo</h1>R${data.receitaAnalitycs.valorReceita}
      </main>
    </div>
  );
}
