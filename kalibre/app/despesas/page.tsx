"use client";
import { Pencil, Trash } from "@phosphor-icons/react";
import Despasas from "../components/Despesas";
import { useEffect, useState } from "react";
import Despesas from "../components/Despesas";




export default function Despesa() {
      
    interface Despesa {
        valor: number,
        data: Date,
        despesaId: Number
    }

    const[despesa,setDespesa] =  useState<Despesa[]>([])
  
    useEffect(()=>{
        const options = {
            method: 'GET',
            headers: {'Content-Type': 'application/json', 'User-Agent': 'insomnia/8.6.1'},
          
          };
          
          fetch('http://172.16.32.16:5014/api/v1/receitas', options)
            .then(response => response.json())
            .then(response => {setDespesa(response);console.log(response)})
            .catch(err => console.error(err));
    },[])

    return (
    <main>
      <div className="p-10">
        <strong className="flex justify-center">
          <h1 className="text-lg">Despesas</h1>
        </strong>
        <div>
          <h2 className="mt-5">Criar nova despesa</h2>
          <form action="" className="flex max-md:flex-col gap-2 mt-5">
            <span>Valor</span>
            <input
              type="number"
              required
              className=" border-stone-300 border-2 max-w-[6rem]  rounded-lg bg-stone-100"
            />
            <span>Data</span>
            <input
              type="date"
              required
              name=""
              id=""
              className=" border-stone-300 border-2  rounded-lg bg-stone-100"
            />
            <button className="bg-sky-500 rounded-md border-2 min-w-[8rem] ml-5 border-transparent text-black hover:bg-transparent hover:border-sky-500 hover:text-sky-500 duration-300 ">
              Registrar
            </button>
          </form>
        </div>
        <div className="mt-10">
        {
            despesa?(
                despesa.map((data)=>{
                    return <Despesas data={data.data} valor={data.valor}></Despesas>
                })
            ):("fdf")
        }
        </div>
      </div>
    </main>
  );
}