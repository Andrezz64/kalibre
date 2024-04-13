export default function FormDespesas(){
    return(
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
    )
}