import React, { useState } from 'react';

export default function Header() {
    // Estado para controlar a exibição do menu
    const [menuAberto, setMenuAberto] = useState(false);

    return (
        <header className="bg-black p-2 pl-5">
            <div className="flex items-center justify-between">
                <h1 className="text-white">Kalibre Finanças</h1>
                {/* Ícone de hambúrguer */}
                <button
                    className="text-white  text-2xl mr-10"
                    onClick={() => setMenuAberto(!menuAberto)}
                >
                    &#9776;
                </button>
            </div>
            {/* Renderização condicional do menu */}
            {menuAberto && (
                <nav className="md:border-t-2 border-[#101010]">
                    {/* Conteúdo do menu */}
                    <ul className='text-white mt-2 flex max-md:flex-col gap-10 max-md:gap-2'>
                         <li><a href="/">Dashboard</a></li>
                        <li><a href="/despesas">Despesas</a></li>
                        <li><a href="/receitas">Receitas</a></li>
                        <li><a href="/relatorio">Relatórios</a></li>
                    </ul>
                </nav>
            )}
        </header>
    );
}
