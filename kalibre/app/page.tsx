"use client";
import Image from "next/image";
import Header from "./components/Header";
import { LineChart } from "@mui/x-charts/LineChart";
import { useEffect, useState } from "react";
import {
  blueberryTwilightPaletteDark,
  blueberryTwilightPaletteLight,
  cheerfulFiestaPaletteDark,
} from "@mui/x-charts/colorPalettes";
import { BarChart } from "@mui/x-charts/BarChart";

export default function Home() {
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
                label: "Valor em R$",
                data: [2, 5.5, 2, 8.5, 1.5, 5, 6, 0, 12],
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
                label: "Valor em R$",
                data: [2, 5.5, 2, 8.5, 1.5, 5, 6, 0, 12],
                color:"Red"
              },
              {
                data: [],
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

