"use client";
import Image from "next/image";
import Header from "./components/Header";
import { LineChart } from "@mui/x-charts/LineChart";
import { SetStateAction, useEffect, useState } from "react";
import { cheerfulFiestaPaletteDark } from "@mui/x-charts/colorPalettes";
import { BarChart } from "@mui/x-charts/BarChart";
import Data from "./Data";

export default function Home() {
  const [despesaTotalPorMes, setDespesaTotalPorMes] = useState([0]);
  const [receitaTotalPorMes, setReceitaTotalPorMes] = useState([0]);
  const mesesDoAno = [
    "Janeiro",
    "Fevereiro",
    "Março",
    "Abril",
    "Maio",
    "Junho",
    "Julho",
    "Agosto",
    "Setembro",
    "Outubro",
    "Novembro",
    "Dezembro",
  ];

  useEffect(() => {
    const ListaTemp: SetStateAction<number[]> = [];
    const options = {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "User-Agent": "insomnia/8.6.1",
      },
    };

    fetch(`http://${Data.FetchIp}/api/v1/dashboard`, options)
      .then((response) => response.json())
      .then((response) => {
        if (response.status == "Ok") {
          response.despesaTotalAno.forEach(
            (despesa: { valorTotal: number }) => {
              ListaTemp.push(despesa.valorTotal);
            }
          );

          setDespesaTotalPorMes(ListaTemp);
          const listaReceita: SetStateAction<number[]> = [];
          response.receitaTotalAno.forEach(
            (receita: { valorTotal: number }) => {
              listaReceita.push(receita.valorTotal);
            }
          );
          setReceitaTotalPorMes(listaReceita);
        } else {
          alert("Ocorreu um error:" + response.msg);
        }
      })
      .catch((err) => console.error(err));
  }, []);

  return (
    <main>
      <h1 className=" text-center text-xl mt-10">Meu controle</h1>
      <div className="flex  justify-center flex-wrap">
        <div className="flex  flex-col mt-[7rem]">
          <div className="flex items-center gap-[34%] ">
            <h2 className="text-lg pl-10">Receitas</h2>
            <button className="bg-sky-500 p-1 rounded-md border-2 min-w-[10.6rem] border-transparent text-black hover:bg-transparent hover:border-sky-500 hover:text-sky-500 duration-300 ">
              Registrar Receitas
            </button>
          </div>
          <BarChart
            xAxis={[{ scaleType: "band", data: mesesDoAno }]}
            series={[
              {
                label: "Receitas(R$)",
                data: receitaTotalPorMes,
              },
              {
                data: [],
              },
            ]}
            width={500}
            height={300}
            grid={{ vertical: true, horizontal: true }}
            colors={cheerfulFiestaPaletteDark}
          />
        </div>
        <div className="flex mt-[7rem] flex-col">
          <div className="flex items-center gap-[34%] ">
            <h2 className="text-lg pl-10">Despesas</h2>
            <button className="bg-sky-500 p-1 rounded-md border-2 min-w-[8rem] border-transparent text-black hover:bg-transparent hover:border-sky-500 hover:text-sky-500 duration-300 ">
              Registrar Despesas
            </button>
          </div>
          <BarChart
            xAxis={[{ scaleType: "band", data: mesesDoAno }]}
            series={[
              {
                label: "Despesas(R$)",
                data: despesaTotalPorMes,
                color: "Red",
              },
              {
                data: receitaTotalPorMes,
              },
            ]}
            width={500}
            height={300}
            grid={{ vertical: true, horizontal: true }}
          />
        </div>
      </div>
    </main>
  );
}
