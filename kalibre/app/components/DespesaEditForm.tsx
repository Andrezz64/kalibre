import { useState } from "react";
import Data from "../Data";

export default function DespesaEditForm(props:any){
    
    const [valor,setValor] = useState(props.valor);
    const [data,setData] = useState(props.data);

    const editDespesa = (e:any) =>{
      console.log(valor,data)  
      e.preventDefault()
        const options = {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
              'X-Api-Key': 'IpKH1FJ7lw5Canu8cvA5T2AGsLdivGjt2PDm09Uz'
            },
            body: `{"despesaId":${props.despesaid},"valor":${valor.replace(/,/g,".")},"data":"${data}"}`
          };
          
          fetch(`http://${Data.FetchIp}/api/v1/despesas/`+props.despesaid, options)
            .then(response => response.json())
            .then(response => console.log(response))
            .catch(err => console.error(err));
            location.reload()
    }
    
    
    return(

         <form onSubmit={editDespesa} className="flex flex-col justify-center items-center  gap-2 mt-5">
        <span className="text-start">Valor</span>
        <input
          type="text"
          value={valor}
          onChange={(e:any)=>{setValor(e.target.value)}}
          required
          className=" border-stone-300 border-2   rounded-lg bg-stone-100"
        />
        <span>Data</span>
        <input
          type="datetime-local"
        
          required
          value={data}
          onChange={(e:any)=>{setData(e.target.value)}}
          name=""
          id=""
          className=" border-stone-300 border-2  rounded-lg bg-stone-100"
        />
        <button className="bg-sky-500 rounded-md border-2 min-w-[8rem] border-transparent text-black hover:bg-transparent hover:border-sky-500 hover:text-sky-500 duration-300 ">
          Editar
        </button>
      </form>

    )
}