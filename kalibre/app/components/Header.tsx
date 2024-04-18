"use client";
import { Crosshair } from "@phosphor-icons/react";
import Link from "next/link";
import React, { useEffect, useState } from "react";

export default function Header() {
  // Estado para controlar a exibição do menu
  const [menuAberto, setMenuAberto] = useState(false);
  useEffect(() => {
    if (window.navigator.userAgent.includes("Mobile") == false) {
      setMenuAberto(true);
    }
  });
  return (
    <header className="bg-black p-2 pl-5">
      <div className="flex items-center justify-between">
        <h1 className="text-white flex items-center gap-2"> <Crosshair size={30} color="#ff0505" />Kalibre Finanças</h1>
        {/* Ícone de hambúrguer */}
        <button
          className="text-white  text-2xl mr-10 md:hidden"
          onClick={() => setMenuAberto(!menuAberto)}
        >
          &#9776;
        </button>
      </div>
      {/* Renderização condicional do menu */}
      {menuAberto && (
        <nav className="md:border-t-2 border-[#101010]">
          {/* Conteúdo do menu */}
          <ul className="text-white mt-2 flex max-md:flex-col gap-10 max-md:gap-2">
            <li>
              <Link href="/">Dashboard</Link>
            </li>
            <li>
              <Link href="/despesas">Despesas</Link>
            </li>
            <li>
              <Link href="/receitas">Receitas</Link>
            </li>
            <li>
              <Link href="/relatorio">Relatórios</Link>
            </li>
          </ul>
        </nav>
      )}
    </header>
  );
}
