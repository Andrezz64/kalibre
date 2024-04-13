import { Pencil, Trash } from "@phosphor-icons/react/dist/ssr";

export default function Despesas(props:any){
    return(
        <div className="bg-stone-200 max-w-[14rem] p-2 rounded-lg shadow-lg">
        <h2>{props.data}</h2>
        <h2>
          Valor: <span className="text-green-500">R${props.valor}</span>
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