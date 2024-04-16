"use client";
import { CircleNotch } from "@phosphor-icons/react";
import { useEffect, useState } from "react";
import Despesas from "../components/Despesas";
import Data from "../Data";

export default function Despesa() {
  interface Despesa {
    valor: number;
    data: Date;
    despesaId: Number;
  }

  const [valor, setValor] = useState("");
  const [data, setData] = useState("");

  function atualizarDespesas() {
    const options = {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "User-Agent": "insomnia/8.6.1",
      },
    };

    fetch(`http://${Data.FetchIp}/api/v1/despesas`, options)
      .then((response) => response.json())
      .then((response) => {
        setDespesa(response.data);
        console.log(response);
      })
      .catch((err) => console.error(err));
  }

  const handleSubmit = async (e: any) => {
    //const ConverterDate = new Date(data)

    e.preventDefault();
    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: `{"valor":${valor.replace(/,/g,".")},"data":"${data}"}`,
    };

    await fetch("http://172.16.32.16:5014/api/v1/despesas", options)
      .then((response) => response.json())
      .then((response) => {
        console.log(response);
        if (response.status == "Ok") {
          console.log("sucesso");

          atualizarDespesas();
        } else {
          alert("Ocorreu um error");
        }
      })
      .catch((err) => console.error(err));
  };

  const [despesa, setDespesa] = useState<Despesa[]>();

  useEffect(() => {
    atualizarDespesas();
  }, []);

  return (
    <main>
      <div className="p-10">
        <strong className="flex justify-center">
          <h1 className="text-lg">Despesas</h1>
        </strong>
        {/* <FormDespesas></FormDespesas> */}
        <div className="border-b-2 border-[#191919] pb-[2rem]">
          <h2 className="mt-5">Criar nova despesa</h2>
          <form
            onSubmit={handleSubmit}
            className="flex max-md:flex-col gap-2 mt-5"
          >
            <span>Valor</span>
            <input
              onChange={(e) => {
                setValor(e.target.value);
              }}
              type="text"
              required
              className=" border-stone-300 border-2 max-w-[6rem]  rounded-lg bg-stone-100"
            />
            <span>Data</span>
            <input
              type="datetime-local"
              required
              onChange={(e) => {
                setData(e.target.value);
              }}
              name=""
              id=""
              className=" border-stone-300 border-2  rounded-lg bg-stone-100"
            />
            <button className="bg-sky-500 rounded-md border-2 min-w-[8rem] ml-5 border-transparent text-black hover:bg-transparent hover:border-sky-500 hover:text-sky-500 duration-300 ">
              Registrar
            </button>
          </form>
        </div>
        <div className="mt-10 flex flex-wrap max-md:justify-center gap-5">
          {despesa ? (
            despesa.map((data) => {
              return (
                <Despesas
                  despesaId={data.despesaId}
                  data={data.data}
                  valor={data.valor}
                ></Despesas>
              );
            })
          ) : (
            <div className="flex justify-start w-screen ml-[50%] mt-[12rem]">
              <CircleNotch size={32} className="animate-spin duration-75 " />
            </div>
          )}
        </div>
      </div>
    </main>
  );
}
