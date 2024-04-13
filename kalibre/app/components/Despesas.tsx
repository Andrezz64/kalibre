'use client'
import { Pencil, Trash } from "@phosphor-icons/react/dist/ssr";
import Despesa from "../models/DespasaModel";
import { useState } from "react";

export default function Despesas(props: Despesa){
  
  const NovaInstanciaData = new Date(props.data)
  const DataFormatada = NovaInstanciaData.toLocaleString("pt-BR")

  // const[dataFormatada,setDataFormatada] = useState(props.data.getHours())
  

  
    return(
        <div className="bg-stone-200 max-w-[14rem] max-h-[10rem] p-2 rounded-lg shadow-lg">
        <h2>{DataFormatada}</h2>
        <h2>
          Valor: <span className="text-green-500">R${props.valor.toString()}</span>
        </h2>
        <div className="flex gap-3 mt-1">
          <button className="text-red-500">
            <Trash size={26} />
          </button>
          <button>
            <Pencil size={26} />
          </button>
        </div>
      </div>
    )
}