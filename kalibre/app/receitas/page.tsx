"use client";
import { CircleNotch } from "@phosphor-icons/react";
import { useEffect, useState } from "react";
import Receitas from "../components/Receitas";
import Data from "../Data";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer, toast } from "react-toastify";
export default function Receita() {
  interface Receita {
    valor: number;
    data: Date;
    receitaid: number;
  }

  const [valor, setValor] = useState("");
  const [data, setData] = useState("");
  const [receita, setReceita] = useState<Receita[]>();

  function atualizarReceitas() {
    const options = {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "User-Agent": "insomnia/8.6.1",
      },
    };

    fetch(`http://${Data.FetchIp}/api/v1/receitas`, options)
      .then((response) => response.json())
      .then((response) => {
        setReceita(response.data);
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
      body: `{"valor":${valor.replace(/,/g, ".")},"data":"${data}"}`,
    };

    await fetch(`http://${Data.FetchIp}/api/v1/receitas`, options)
      .then((response) => response.json())
      .then((response) => {
        if (response.status == "Ok") {
          toast.success("Receita cadastrada!");
          atualizarReceitas();
        } else {
          toast.error("Ops! Ocorreu um error: " + response.msg);
        }
      })
      .catch((err) => console.error(err));
  };

  useEffect(() => {
    atualizarReceitas();
  }, []);

  return (
    <main>
      <div className="p-10">
        <strong className="flex justify-center">
          <h1 className="text-lg">Receitas</h1>
        </strong>

        <div className="border-b-2 border-[#191919] pb-[2rem]">
          <h2 className="mt-5">Criar nova receita</h2>
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
          {receita ? (
            receita.map((data) => {
              return (
                <Receitas
                  receitaId={data.receitaid}
                  data={data.data}
                  valor={data.valor}
                ></Receitas>
              );
            })
          ) : (
            <div className="flex justify-start w-screen ml-[50%] mt-[12rem]">
              <CircleNotch size={32} className="animate-spin duration-75 " />
            </div>
          )}
        </div>
        <ToastContainer />
      </div>
    </main>
  );
}
