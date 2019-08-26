﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;

namespace MegaSolucao.Infraestrutura
{
    public static class Sessao
    {
        static Sessao()
        {
            Configuracao = new Configuracao
            {
                ConexaoAsterisk = new ConexaoAsterisk
                {
                    HostDaAplicacao = "192.168.15.149",
                    HostDoAsterisk = "192.168.15.204",
                    PortaDoAsterisk = 5038,
                    LoginDoAsterisk = "snep",
                    SenhaDoAsterisk = "sneppass"
                },
                ConexaoRavenDB = new ConexaoRavenDB
                {
                    Servidor = @"localhost:32772",
                    NomeDoBanco = "Test"
                },
                CooldownExecutarLigacoes = TimeSpan.FromSeconds(10)
            };
        }

        public static Configuracao Configuracao { get; set; }
    }
}
