"use client";
import Image from "next/image";
import Header from "./components/Header";
import { LineChart } from "@mui/x-charts/LineChart";
import { SetStateAction, useEffect, useState } from "react";
import { cheerfulFiestaPaletteDark } from "@mui/x-charts/colorPalettes";
import { BarChart } from "@mui/x-charts/BarChart";
import Data from "./Data";
import Grafico from "./components/Grafico";

export default function Home() {
  const [despesaTotalPorMes, setDespesaTotalPorMes] = useState([0]);
  const [receitaTotalPorMes, setReceitaTotalPorMes] = useState([0]);

  const [value, setValue] = useState(0);
  const handleChange = (e: any) => {
    setValue(e.target.value);
  };

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
      <div className="flex justify-center flex-col items-center">
        <h1 className=" text-center text-xl mt-10 mb-10">Meu controle</h1>
        <div>
          <select
            value={value}
            name="graficos"
            id="grafico"
            onChange={handleChange}
            className="bg-gray-200 rounded-lg p-[0.20rem]"
          >
            <option value={0}>Gráfico de despesas</option>
            <option value={1}>Gráfico de receitas</option>
            <option value={2}>Gráfico Receitas e Despesas</option>
          </select>

          {value == 0 && (
            <Grafico
              label={"Despesas(R$)"}
              color="red"
              data={despesaTotalPorMes}
              _data={[]}
            />
          )}
          {value == 1 && (
            <Grafico
              label={"Receitas(R$)"}
              color="green"
              data={receitaTotalPorMes}
              _data={[]}
            />
          )}
          {value == 2 && (
            <Grafico
              label={"Receitas(R$)"}
              color="green"
              data={receitaTotalPorMes}
              _label="Despesas(R$)"
              _data={despesaTotalPorMes}
            />
          )}
        </div>
      </div>
    </main>
  );
}
