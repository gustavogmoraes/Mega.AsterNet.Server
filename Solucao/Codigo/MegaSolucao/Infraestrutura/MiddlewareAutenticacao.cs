﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MegaSolucao.Infraestrutura
{
    public class MiddlewareAutenticacao
    {
        private readonly RequestDelegate _next;

        public MiddlewareAutenticacao(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = ObtenhaTokenDaUrl(context.Request.Path);

            if (!Guid.TryParse(token, out var guidToken))
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Requisição sem token");

                return;
            }

            if (!Autenticacao.ValidateToken(guidToken).Result)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Usuário não autenticado!");

                return;
            }

            RemovaToken(context, token);
            await _next.Invoke(context);
        }

        private string ObtenhaTokenDaUrl(PathString caminho)
        {
            return caminho.Value.Split('/')[4];
        }

        private void RemovaToken(HttpContext context, string token)
        {
            var caminhoSemToken = context.Request.Path.Value.Remove(context.Request.Path.Value.IndexOf(token, StringComparison.Ordinal), token.Length);
            if (caminhoSemToken.Contains("//"))
            {
                caminhoSemToken = caminhoSemToken.Remove(caminhoSemToken.IndexOf("//", StringComparison.Ordinal), 1);
            }

            context.Request.Path = new PathString(caminhoSemToken);
        }
    }
}
