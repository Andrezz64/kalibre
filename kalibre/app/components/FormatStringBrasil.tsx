export default function FormatStringBrasil(props: any) {
  return props.valor.toLocaleString("pt-BR", {
    style: "currency",
    currency: "BRL",
  });
}
