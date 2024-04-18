import FormatStringBrasil from "./FormatStringBrasil";

export default function RelatorioComponent(props: any) {
  return (
    <div className="flex gap-7 flex-wrap ">
      <div className="flex items-center flex-col">
        <h1>Despesas geradas no periodo</h1>
        <span className="text-red-500">{props.quantidadeDespesa}</span>
      </div>
      <div className="flex items-center flex-col">
        <h1>Valor em despesas no periodo</h1>
        <span className="text-red-500">
          <FormatStringBrasil valor={props.valorDespesa} />
        </span>
      </div>

      <div className="flex items-center flex-col">
        <h1>Receitas geradas no periodo</h1>{" "}
        <span className="text-green-500">{props.quantidadeReceita}</span>
      </div>
      <div className="flex items-center flex-col">
        <h1>Valor em receitas no periodo</h1>
        <span className="text-green-500">
          <FormatStringBrasil valor={props.valorReceita} />
        </span>
      </div>
    </div>
  );
}
