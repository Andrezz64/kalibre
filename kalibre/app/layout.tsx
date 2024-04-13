"use client"
import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import Header from "./components/Header";

const inter = Inter({ subsets: ["latin"] });

// export const metadata: Metadata = {
//   title: "Kalibre",
//   description: "Generated by create next app",
// };

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <title>Kalibre</title>
      <body className={inter.className}>
      <Header></Header>
        {children}</body>
    </html>
  );
}
